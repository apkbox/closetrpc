TODO:
* In the current model client does not guarantee call ordering between threads.

* Generate base class for service implementation that derive from both
  the service interface and the stub. This base class should have
  protected constructor. This will help to avoid creating stub unnceessarily
  as the base class implementation is derived from stub.

* Channel must operate with buffers instead of messages. The message should
  already be serialized by the server before passing it to the channel. So,
  the channel is a transport only.

* The service implementation may not be thread safe by default, provide
  and easy way to serialize calls to the service if needed, probably using
  a stub wrapper. The default server behavior is that all incoming calls 
  processed asynchronously and a call may be received and handled while
  another call is in progress. One should be able to specify behavior
  on per service basis rather than per server.

* Whenever Message::ByteSize is called the next call of Message::GetCachedSize 
  can save time. This is often the case when writing into WriteBuffer - note
  that size is used twice:
    auto size = msg.ByteSize();
    msg.SerializePartialToArray(message->Write(size), size);

* Add client token that allows to uniquely identify client connection
  on the server. This will allow to restore server side client state, for
  example event subscriptions, cache etc. This though will require some sort
  of timeout after which server can discard the context. This will also require
  the client to be able to handle reconnection for the case when server already
  dumped the client context due to timeout or some other reasons.

* There are many different transports out there, one discriminator is
  connection cardinality.
    - Point-to-point transports (RS232 for example)
    - One-to-many transports (TCP/IP sockets, pipe)
  Connection attempt on the server side can result in the following results:
    - Succeeded
    - Failed (no more connections available) - further attempt will fail
    - Failed (temporary issue) - further attempts may succeed.
  To handle p2p transports make transport to return maximum number of
  connections it can satisfy - [1..Inf]. This will fit perfectly into server
  provided restriction on numer of connections like below:
```c++
    while (true) {
      if (n_conn < min(transport.max_conn(), server.max_conn())) {
        /* connect */
        ++n_conn;
      }
      else {
        /* refuse connection */
      }
    }
```

OTHER THOUGHTS:
* object_id_value and event_name fields included into RpcParameter apparently
  to make object manager and event service looks as any other service and yet
  have strongly typed and expected parameters.
  Thus it seems to be better to move these fields to RpcCall or RpcMessage.

* Not sure why is_null was needed. Does not seem used anywhere.

* With proto3 Any type is better suited to store a parameter.
  * This is irrelevant for closet-rpc because it uses protobuf independent
    serialization for control messages.

* When exception happen on the server while handling service call the following
  strategies can be used:
  * Let exception handling method of service to handle it 
    and prepare a result data that could be understood by the client. The server
    returns that data with `ApplicationError` status.
  * Let exception handled through client callback (one for all services.)
  * Combine both approaches - let service handle if first and then invoke a
    global handler if not.

* Once connection established, give the server's user code a chance to handle
  messages on a separate service stack, which then indicates whether
  the connection can proceed or should be aborted. This can serve as a plug in
  point for custom authorization/authentication or version checking.

## Extracted from source code
### client.cpp::FlushPendingCalls
    // Graceful shutdown keeps channel open until all calls complete
    // (but prevents sending new ones).
    // However, if channel closes, e.g. Disconnect called from other thread
    // while Shutdown is blocked, we want to exit, because the following wait
    // will never be satisfied by HandleIncomingMessage and outstanding calls,
    // that not yet dispatched, will not complete.

### client.cpp::HandleIncomingMessage
      // TODO: Implement queue size limiting. Drop the oldest events.
      // BUG: Although it looks smart to cache the handler
      // pointer to avoid extra lookup it is a bad move.
      // Between time we received event and user called
      // PumpEvent, the other thread coild have called StopListening
      // so, the pointer to the service will be invalid.
      // On the other hand StopListening should reliably remove
      // events that are not being listened.

### client.h
    // TODO: Big question is whether implicit connect is allowed.
    // Should it be a config property (enabled by default) or not?
    // Should client stick to RAII (so, if connection is lost, the only option
    // is to create a new Client)?

### server.cpp::Wait
  // TODO: Prevent calling Wait concurrently. There could be only one thread.

### client.h::CallMethod
  // Calls RPC method and waits for reply.
  // TODO: If call is asynchronous the method should not block.
  // However, only proxy/stub know that the call is async, so
  // we should relay this information to CallMethod either within
  // RpcCall/RpcMessage (previous implementation) or via additional parameter.


### ServiceManager
    // TODO: Methods of this class should be thread-safe, because on the client
    // these can be called concurrently from app threads while
    // registering/unregistering event listeners and from incoming messages thread
    // while looking up for event routing.
    // TODO: Make it a template so we can specify locking policy.
    // This is needed for per-context service manager as it does not require locking.

### ServerContext::SendEvent
  // TODO: This is quite inefficient here, because the call is serialized
  // for each context (connection). It would be nice to cache the serialized
  // version and then send it for each context.
  // Note that as implementation moved towared connection based handling,
  // this inefficiency only matter when broadcasting events.

### client Event handling
    // TODO: Implement queue size limiting. Drop the oldest events.
    // BUG: Although it looks smart to cache the handler
    // pointer to avoid extra lookup it is a bad move.
    // Between time we received event and user called
    // PumpEvent, the other thread coild have called StopListening
    // so, the pointer to the service will be invalid.
    // On the other hand StopListening should reliably remove
    // events that are not being listened.

