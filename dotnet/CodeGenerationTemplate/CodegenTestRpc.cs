// --------------------------------------------------------------------------------
// <copyright file="CodegenTestRpc.cs" company="Private">
//   Copyright (c) Alex Kozlov.
// </copyright>
// <summary>
//   This file serves as a test bed for Protocol Buffers RPC template.
//   The protoc plugin code generation is based on this code.
// </summary>
// --------------------------------------------------------------------------------

namespace CodegenTest
{
    using System;

    using ClosetRpc.Net;

    using Google.Protobuf;
    using Google.Protobuf.WellKnownTypes;

    public interface TestServiceInterface
    {
        #region Public Methods and Operators

        void Method_V_V(ServerContext context);

        void Method_V_M(ServerContext context, StructType value);

        StructType Method_M_V(ServerContext context);

        StructType Method_M_M(ServerContext context, StructType value);

        void AsyncMethod_V_V(ServerContext context);

        void AsyncMethod_V_M(ServerContext context, StructType value);

        #endregion
    }

    public abstract class TestService_StubBase : IRpcService
    {
        public string Name
        {
            get
            {
                return "TestService";
            }
        }

        protected abstract TestServiceInterface Impl { get; }

        public void CallMethod(ServerContext context, IRpcCall rpcCall, IRpcResult rpcResult)
        {
            rpcResult.Status = RpcStatus.Succeeded;

            if (rpcCall.MethodName == "Method_V_V")
            {
                this.Impl.Method_V_V(context);
            }
            else if (rpcCall.MethodName == "Method_V_M")
            {
                var intput = new StructType();
                intput.MergeFrom(new CodedInputStream(rpcCall.CallData));
                this.Impl.Method_V_M(context, intput);
                rpcResult.ResultData = new Empty().ToByteArray();
            }
            else if (rpcCall.MethodName == "Method_M_V")
            {
                var result = this.Impl.Method_M_V(context);
                rpcResult.ResultData = result.ToByteArray();
            }
            else if (rpcCall.MethodName == "Method_M_M")
            {
                var intput = new StructType();
                intput.MergeFrom(new CodedInputStream(rpcCall.CallData));
                var result = this.Impl.Method_M_M(context, intput);
                rpcResult.ResultData = result.ToByteArray();
            }
            else if (rpcCall.MethodName == "AsyncMethod_V_V")
            {
                this.Impl.AsyncMethod_V_V(context);
            }
            else if (rpcCall.MethodName == "AsyncMethod_V_M")
            {
                var intput = new StructType();
                intput.MergeFrom(new CodedInputStream(rpcCall.CallData));
                this.Impl.AsyncMethod_V_M(context, intput);
            }
            else
            {
                rpcResult.Status = RpcStatus.UnknownMethod;
            }
        }
    }

    public class TestService_Stub : TestService_StubBase
    {
        public TestService_Stub(TestServiceInterface impl)
        {
            this.Impl = impl;
        }

        protected override TestServiceInterface Impl { get; }
    }

    public abstract class TestService_ServiceBase : TestService_StubBase, TestServiceInterface
    {
        protected override TestServiceInterface Impl
        {
            get
            {
                return this;
            }
        }

        public abstract void Method_V_V(ServerContext context);

        public abstract void Method_V_M(ServerContext context, StructType value);

        public abstract StructType Method_M_V(ServerContext context);

        public abstract StructType Method_M_M(ServerContext context, StructType value);

        public abstract void AsyncMethod_V_V(ServerContext context);

        public abstract void AsyncMethod_V_M(ServerContext context, StructType value);
    }

    public class TestServiceInterface_Proxy
    {
        private static readonly string ServiceName = "TestServiceInterface";

        private readonly Client client;

        public TestServiceInterface_Proxy(Client client)
        {
            this.client = client;
        }

        #region Public Methods and Operators

        public void Method_V_V()
        {
            var call = this.client.CreateCallBuilder();
            call.ServiceName = TestServiceInterface_Proxy.ServiceName;
            call.MethodName = "Method_V_V";
            var result = this.client.CallService(call);
            if (result.Status != RpcStatus.Succeeded)
            {
                throw new Exception(); // TODO: Be more specific
            }
        }

        public void Method_V_M(StructType value)
        {
            var call = this.client.CreateCallBuilder();
            call.ServiceName = TestServiceInterface_Proxy.ServiceName;
            call.MethodName = "Method_V_M";
            call.CallData = value.ToByteArray();
            var result = this.client.CallService(call);
            if (result.Status != RpcStatus.Succeeded)
            {
                throw new Exception(); // TODO: Be more specific
            }
        }

        public StructType Method_M_V()
        {
            var call = this.client.CreateCallBuilder();
            call.ServiceName = TestServiceInterface_Proxy.ServiceName;
            call.MethodName = "Method_M_V";
            var result = this.client.CallService(call);
            if (result.Status != RpcStatus.Succeeded)
            {
                throw new Exception(); // TODO: Be more specific
            }

            var returnValue = new StructType();
            returnValue.MergeFrom(result.ResultData);
            return returnValue;
        }

        public StructType Method_M_M(ServerContext context, StructType value)
        {
            var call = this.client.CreateCallBuilder();
            call.ServiceName = TestServiceInterface_Proxy.ServiceName;
            call.MethodName = "Method_M_M";
            call.CallData = value.ToByteArray();
            var result = this.client.CallService(call);
            if (result.Status != RpcStatus.Succeeded)
            {
                throw new Exception(); // TODO: Be more specific
            }

            var returnValue = new StructType();
            returnValue.MergeFrom(result.ResultData);
            return returnValue;
        }

        public void AsyncMethod_V_V(ServerContext context)
        {
            var call = this.client.CreateCallBuilder();
            call.ServiceName = TestServiceInterface_Proxy.ServiceName;
            call.MethodName = "AsyncMethod_V_V";
            call.IsAsync = true;
            var result = this.client.CallService(call);
            if (result.Status != RpcStatus.Succeeded)
            {
                throw new Exception(); // TODO: Be more specific
            }
        }

        public void AsyncMethod_V_M(ServerContext context, StructType value)
        {
            var call = this.client.CreateCallBuilder();
            call.ServiceName = TestServiceInterface_Proxy.ServiceName;
            call.MethodName = "AsyncMethod_V_M";
            call.IsAsync = true;
            call.CallData = value.ToByteArray();
            var result = this.client.CallService(call);
            if (result.Status != RpcStatus.Succeeded)
            {
                throw new Exception(); // TODO: Be more specific
            }
        }

        #endregion
    }
}
