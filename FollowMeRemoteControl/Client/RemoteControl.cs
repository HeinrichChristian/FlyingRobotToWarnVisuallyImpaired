﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:4.0.30319.34209
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This code was auto-generated by SlSvcUtil, version 3.7.0.0
// 
namespace FollowMe.Enums
{
    using System.Runtime.Serialization;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="TargetLocation", Namespace="http://schemas.datacontract.org/2004/07/FollowMe.Enums")]
    public enum TargetLocation : int
    {
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        Unknown = 0,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TopLeft = 1,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TopCenter = 2,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        TopRight = 3,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CenterLeft = 4,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CenterCenter = 5,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        CenterRight = 6,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BottomLeft = 7,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BottomCenter = 8,
        
        [System.Runtime.Serialization.EnumMemberAttribute()]
        BottomRight = 9,
    }
}
namespace FollowMe.WebService
{
    using System.Runtime.Serialization;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PersonAndDangerLocation", Namespace="http://schemas.datacontract.org/2004/07/FollowMe.WebService")]
    public partial class PersonAndDangerLocation : object
    {
        
        private FollowMe.Enums.TargetLocation DangerLocationField;
        
        private FollowMe.Enums.TargetLocation PersonLocationField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FollowMe.Enums.TargetLocation DangerLocation
        {
            get
            {
                return this.DangerLocationField;
            }
            set
            {
                this.DangerLocationField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public FollowMe.Enums.TargetLocation PersonLocation
        {
            get
            {
                return this.PersonLocationField;
            }
            set
            {
                this.PersonLocationField = value;
            }
        }
    }
}


[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(ConfigurationName="IRemoteControl")]
public interface IRemoteControl
{
    
    [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IRemoteControl/Start", ReplyAction="http://tempuri.org/IRemoteControl/StartResponse")]
    System.IAsyncResult BeginStart(System.AsyncCallback callback, object asyncState);
    
    void EndStart(System.IAsyncResult result);
    
    [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IRemoteControl/Stop", ReplyAction="http://tempuri.org/IRemoteControl/StopResponse")]
    System.IAsyncResult BeginStop(System.AsyncCallback callback, object asyncState);
    
    void EndStop(System.IAsyncResult result);
    
    [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IRemoteControl/GetPersonLocation", ReplyAction="http://tempuri.org/IRemoteControl/GetPersonLocationResponse")]
    System.IAsyncResult BeginGetPersonLocation(System.AsyncCallback callback, object asyncState);
    
    FollowMe.Enums.TargetLocation EndGetPersonLocation(System.IAsyncResult result);
    
    [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IRemoteControl/GetDangerLocation", ReplyAction="http://tempuri.org/IRemoteControl/GetDangerLocationResponse")]
    System.IAsyncResult BeginGetDangerLocation(System.AsyncCallback callback, object asyncState);
    
    FollowMe.Enums.TargetLocation EndGetDangerLocation(System.IAsyncResult result);
    
    [System.ServiceModel.OperationContractAttribute(AsyncPattern=true, Action="http://tempuri.org/IRemoteControl/GetPersonAndDangerLocation", ReplyAction="http://tempuri.org/IRemoteControl/GetPersonAndDangerLocationResponse")]
    System.IAsyncResult BeginGetPersonAndDangerLocation(System.AsyncCallback callback, object asyncState);
    
    FollowMe.WebService.PersonAndDangerLocation EndGetPersonAndDangerLocation(System.IAsyncResult result);
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface IRemoteControlChannel : IRemoteControl, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class GetPersonLocationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{
    
    private object[] results;
    
    public GetPersonLocationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState)
    {
        this.results = results;
    }
    
    public FollowMe.Enums.TargetLocation Result
    {
        get
        {
            base.RaiseExceptionIfNecessary();
            return ((FollowMe.Enums.TargetLocation)(this.results[0]));
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class GetDangerLocationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{
    
    private object[] results;
    
    public GetDangerLocationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState)
    {
        this.results = results;
    }
    
    public FollowMe.Enums.TargetLocation Result
    {
        get
        {
            base.RaiseExceptionIfNecessary();
            return ((FollowMe.Enums.TargetLocation)(this.results[0]));
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class GetPersonAndDangerLocationCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs
{
    
    private object[] results;
    
    public GetPersonAndDangerLocationCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
            base(exception, cancelled, userState)
    {
        this.results = results;
    }
    
    public FollowMe.WebService.PersonAndDangerLocation Result
    {
        get
        {
            base.RaiseExceptionIfNecessary();
            return ((FollowMe.WebService.PersonAndDangerLocation)(this.results[0]));
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class RemoteControlClient : System.ServiceModel.ClientBase<IRemoteControl>, IRemoteControl
{
    
    private BeginOperationDelegate onBeginStartDelegate;
    
    private EndOperationDelegate onEndStartDelegate;
    
    private System.Threading.SendOrPostCallback onStartCompletedDelegate;
    
    private BeginOperationDelegate onBeginStopDelegate;
    
    private EndOperationDelegate onEndStopDelegate;
    
    private System.Threading.SendOrPostCallback onStopCompletedDelegate;
    
    private BeginOperationDelegate onBeginGetPersonLocationDelegate;
    
    private EndOperationDelegate onEndGetPersonLocationDelegate;
    
    private System.Threading.SendOrPostCallback onGetPersonLocationCompletedDelegate;
    
    private BeginOperationDelegate onBeginGetDangerLocationDelegate;
    
    private EndOperationDelegate onEndGetDangerLocationDelegate;
    
    private System.Threading.SendOrPostCallback onGetDangerLocationCompletedDelegate;
    
    private BeginOperationDelegate onBeginGetPersonAndDangerLocationDelegate;
    
    private EndOperationDelegate onEndGetPersonAndDangerLocationDelegate;
    
    private System.Threading.SendOrPostCallback onGetPersonAndDangerLocationCompletedDelegate;
    
    private BeginOperationDelegate onBeginOpenDelegate;
    
    private EndOperationDelegate onEndOpenDelegate;
    
    private System.Threading.SendOrPostCallback onOpenCompletedDelegate;
    
    private BeginOperationDelegate onBeginCloseDelegate;
    
    private EndOperationDelegate onEndCloseDelegate;
    
    private System.Threading.SendOrPostCallback onCloseCompletedDelegate;
    
    public RemoteControlClient()
    {
    }
    
    public RemoteControlClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public RemoteControlClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public RemoteControlClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public RemoteControlClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    public System.Net.CookieContainer CookieContainer
    {
        get
        {
            System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
            if ((httpCookieContainerManager != null))
            {
                return httpCookieContainerManager.CookieContainer;
            }
            else
            {
                return null;
            }
        }
        set
        {
            System.ServiceModel.Channels.IHttpCookieContainerManager httpCookieContainerManager = this.InnerChannel.GetProperty<System.ServiceModel.Channels.IHttpCookieContainerManager>();
            if ((httpCookieContainerManager != null))
            {
                httpCookieContainerManager.CookieContainer = value;
            }
            else
            {
                throw new System.InvalidOperationException("Unable to set the CookieContainer. Please make sure the binding contains an HttpC" +
                        "ookieContainerBindingElement.");
            }
        }
    }
    
    public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> StartCompleted;
    
    public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> StopCompleted;
    
    public event System.EventHandler<GetPersonLocationCompletedEventArgs> GetPersonLocationCompleted;
    
    public event System.EventHandler<GetDangerLocationCompletedEventArgs> GetDangerLocationCompleted;
    
    public event System.EventHandler<GetPersonAndDangerLocationCompletedEventArgs> GetPersonAndDangerLocationCompleted;
    
    public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> OpenCompleted;
    
    public event System.EventHandler<System.ComponentModel.AsyncCompletedEventArgs> CloseCompleted;
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.IAsyncResult IRemoteControl.BeginStart(System.AsyncCallback callback, object asyncState)
    {
        return base.Channel.BeginStart(callback, asyncState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    void IRemoteControl.EndStart(System.IAsyncResult result)
    {
        base.Channel.EndStart(result);
    }
    
    private System.IAsyncResult OnBeginStart(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((IRemoteControl)(this)).BeginStart(callback, asyncState);
    }
    
    private object[] OnEndStart(System.IAsyncResult result)
    {
        ((IRemoteControl)(this)).EndStart(result);
        return null;
    }
    
    private void OnStartCompleted(object state)
    {
        if ((this.StartCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.StartCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void StartAsync()
    {
        this.StartAsync(null);
    }
    
    public void StartAsync(object userState)
    {
        if ((this.onBeginStartDelegate == null))
        {
            this.onBeginStartDelegate = new BeginOperationDelegate(this.OnBeginStart);
        }
        if ((this.onEndStartDelegate == null))
        {
            this.onEndStartDelegate = new EndOperationDelegate(this.OnEndStart);
        }
        if ((this.onStartCompletedDelegate == null))
        {
            this.onStartCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnStartCompleted);
        }
        base.InvokeAsync(this.onBeginStartDelegate, null, this.onEndStartDelegate, this.onStartCompletedDelegate, userState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.IAsyncResult IRemoteControl.BeginStop(System.AsyncCallback callback, object asyncState)
    {
        return base.Channel.BeginStop(callback, asyncState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    void IRemoteControl.EndStop(System.IAsyncResult result)
    {
        base.Channel.EndStop(result);
    }
    
    private System.IAsyncResult OnBeginStop(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((IRemoteControl)(this)).BeginStop(callback, asyncState);
    }
    
    private object[] OnEndStop(System.IAsyncResult result)
    {
        ((IRemoteControl)(this)).EndStop(result);
        return null;
    }
    
    private void OnStopCompleted(object state)
    {
        if ((this.StopCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.StopCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void StopAsync()
    {
        this.StopAsync(null);
    }
    
    public void StopAsync(object userState)
    {
        if ((this.onBeginStopDelegate == null))
        {
            this.onBeginStopDelegate = new BeginOperationDelegate(this.OnBeginStop);
        }
        if ((this.onEndStopDelegate == null))
        {
            this.onEndStopDelegate = new EndOperationDelegate(this.OnEndStop);
        }
        if ((this.onStopCompletedDelegate == null))
        {
            this.onStopCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnStopCompleted);
        }
        base.InvokeAsync(this.onBeginStopDelegate, null, this.onEndStopDelegate, this.onStopCompletedDelegate, userState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.IAsyncResult IRemoteControl.BeginGetPersonLocation(System.AsyncCallback callback, object asyncState)
    {
        return base.Channel.BeginGetPersonLocation(callback, asyncState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    FollowMe.Enums.TargetLocation IRemoteControl.EndGetPersonLocation(System.IAsyncResult result)
    {
        return base.Channel.EndGetPersonLocation(result);
    }
    
    private System.IAsyncResult OnBeginGetPersonLocation(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((IRemoteControl)(this)).BeginGetPersonLocation(callback, asyncState);
    }
    
    private object[] OnEndGetPersonLocation(System.IAsyncResult result)
    {
        FollowMe.Enums.TargetLocation retVal = ((IRemoteControl)(this)).EndGetPersonLocation(result);
        return new object[] {
                retVal};
    }
    
    private void OnGetPersonLocationCompleted(object state)
    {
        if ((this.GetPersonLocationCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.GetPersonLocationCompleted(this, new GetPersonLocationCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void GetPersonLocationAsync()
    {
        this.GetPersonLocationAsync(null);
    }
    
    public void GetPersonLocationAsync(object userState)
    {
        if ((this.onBeginGetPersonLocationDelegate == null))
        {
            this.onBeginGetPersonLocationDelegate = new BeginOperationDelegate(this.OnBeginGetPersonLocation);
        }
        if ((this.onEndGetPersonLocationDelegate == null))
        {
            this.onEndGetPersonLocationDelegate = new EndOperationDelegate(this.OnEndGetPersonLocation);
        }
        if ((this.onGetPersonLocationCompletedDelegate == null))
        {
            this.onGetPersonLocationCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetPersonLocationCompleted);
        }
        base.InvokeAsync(this.onBeginGetPersonLocationDelegate, null, this.onEndGetPersonLocationDelegate, this.onGetPersonLocationCompletedDelegate, userState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.IAsyncResult IRemoteControl.BeginGetDangerLocation(System.AsyncCallback callback, object asyncState)
    {
        return base.Channel.BeginGetDangerLocation(callback, asyncState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    FollowMe.Enums.TargetLocation IRemoteControl.EndGetDangerLocation(System.IAsyncResult result)
    {
        return base.Channel.EndGetDangerLocation(result);
    }
    
    private System.IAsyncResult OnBeginGetDangerLocation(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((IRemoteControl)(this)).BeginGetDangerLocation(callback, asyncState);
    }
    
    private object[] OnEndGetDangerLocation(System.IAsyncResult result)
    {
        FollowMe.Enums.TargetLocation retVal = ((IRemoteControl)(this)).EndGetDangerLocation(result);
        return new object[] {
                retVal};
    }
    
    private void OnGetDangerLocationCompleted(object state)
    {
        if ((this.GetDangerLocationCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.GetDangerLocationCompleted(this, new GetDangerLocationCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void GetDangerLocationAsync()
    {
        this.GetDangerLocationAsync(null);
    }
    
    public void GetDangerLocationAsync(object userState)
    {
        if ((this.onBeginGetDangerLocationDelegate == null))
        {
            this.onBeginGetDangerLocationDelegate = new BeginOperationDelegate(this.OnBeginGetDangerLocation);
        }
        if ((this.onEndGetDangerLocationDelegate == null))
        {
            this.onEndGetDangerLocationDelegate = new EndOperationDelegate(this.OnEndGetDangerLocation);
        }
        if ((this.onGetDangerLocationCompletedDelegate == null))
        {
            this.onGetDangerLocationCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetDangerLocationCompleted);
        }
        base.InvokeAsync(this.onBeginGetDangerLocationDelegate, null, this.onEndGetDangerLocationDelegate, this.onGetDangerLocationCompletedDelegate, userState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.IAsyncResult IRemoteControl.BeginGetPersonAndDangerLocation(System.AsyncCallback callback, object asyncState)
    {
        return base.Channel.BeginGetPersonAndDangerLocation(callback, asyncState);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    FollowMe.WebService.PersonAndDangerLocation IRemoteControl.EndGetPersonAndDangerLocation(System.IAsyncResult result)
    {
        return base.Channel.EndGetPersonAndDangerLocation(result);
    }
    
    private System.IAsyncResult OnBeginGetPersonAndDangerLocation(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((IRemoteControl)(this)).BeginGetPersonAndDangerLocation(callback, asyncState);
    }
    
    private object[] OnEndGetPersonAndDangerLocation(System.IAsyncResult result)
    {
        FollowMe.WebService.PersonAndDangerLocation retVal = ((IRemoteControl)(this)).EndGetPersonAndDangerLocation(result);
        return new object[] {
                retVal};
    }
    
    private void OnGetPersonAndDangerLocationCompleted(object state)
    {
        if ((this.GetPersonAndDangerLocationCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.GetPersonAndDangerLocationCompleted(this, new GetPersonAndDangerLocationCompletedEventArgs(e.Results, e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void GetPersonAndDangerLocationAsync()
    {
        this.GetPersonAndDangerLocationAsync(null);
    }
    
    public void GetPersonAndDangerLocationAsync(object userState)
    {
        if ((this.onBeginGetPersonAndDangerLocationDelegate == null))
        {
            this.onBeginGetPersonAndDangerLocationDelegate = new BeginOperationDelegate(this.OnBeginGetPersonAndDangerLocation);
        }
        if ((this.onEndGetPersonAndDangerLocationDelegate == null))
        {
            this.onEndGetPersonAndDangerLocationDelegate = new EndOperationDelegate(this.OnEndGetPersonAndDangerLocation);
        }
        if ((this.onGetPersonAndDangerLocationCompletedDelegate == null))
        {
            this.onGetPersonAndDangerLocationCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnGetPersonAndDangerLocationCompleted);
        }
        base.InvokeAsync(this.onBeginGetPersonAndDangerLocationDelegate, null, this.onEndGetPersonAndDangerLocationDelegate, this.onGetPersonAndDangerLocationCompletedDelegate, userState);
    }
    
    private System.IAsyncResult OnBeginOpen(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((System.ServiceModel.ICommunicationObject)(this)).BeginOpen(callback, asyncState);
    }
    
    private object[] OnEndOpen(System.IAsyncResult result)
    {
        ((System.ServiceModel.ICommunicationObject)(this)).EndOpen(result);
        return null;
    }
    
    private void OnOpenCompleted(object state)
    {
        if ((this.OpenCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.OpenCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void OpenAsync()
    {
        this.OpenAsync(null);
    }
    
    public void OpenAsync(object userState)
    {
        if ((this.onBeginOpenDelegate == null))
        {
            this.onBeginOpenDelegate = new BeginOperationDelegate(this.OnBeginOpen);
        }
        if ((this.onEndOpenDelegate == null))
        {
            this.onEndOpenDelegate = new EndOperationDelegate(this.OnEndOpen);
        }
        if ((this.onOpenCompletedDelegate == null))
        {
            this.onOpenCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnOpenCompleted);
        }
        base.InvokeAsync(this.onBeginOpenDelegate, null, this.onEndOpenDelegate, this.onOpenCompletedDelegate, userState);
    }
    
    private System.IAsyncResult OnBeginClose(object[] inValues, System.AsyncCallback callback, object asyncState)
    {
        return ((System.ServiceModel.ICommunicationObject)(this)).BeginClose(callback, asyncState);
    }
    
    private object[] OnEndClose(System.IAsyncResult result)
    {
        ((System.ServiceModel.ICommunicationObject)(this)).EndClose(result);
        return null;
    }
    
    private void OnCloseCompleted(object state)
    {
        if ((this.CloseCompleted != null))
        {
            InvokeAsyncCompletedEventArgs e = ((InvokeAsyncCompletedEventArgs)(state));
            this.CloseCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
        }
    }
    
    public void CloseAsync()
    {
        this.CloseAsync(null);
    }
    
    public void CloseAsync(object userState)
    {
        if ((this.onBeginCloseDelegate == null))
        {
            this.onBeginCloseDelegate = new BeginOperationDelegate(this.OnBeginClose);
        }
        if ((this.onEndCloseDelegate == null))
        {
            this.onEndCloseDelegate = new EndOperationDelegate(this.OnEndClose);
        }
        if ((this.onCloseCompletedDelegate == null))
        {
            this.onCloseCompletedDelegate = new System.Threading.SendOrPostCallback(this.OnCloseCompleted);
        }
        base.InvokeAsync(this.onBeginCloseDelegate, null, this.onEndCloseDelegate, this.onCloseCompletedDelegate, userState);
    }
    
    protected override IRemoteControl CreateChannel()
    {
        return new RemoteControlClientChannel(this);
    }
    
    private class RemoteControlClientChannel : ChannelBase<IRemoteControl>, IRemoteControl
    {
        
        public RemoteControlClientChannel(System.ServiceModel.ClientBase<IRemoteControl> client) : 
                base(client)
        {
        }
        
        public System.IAsyncResult BeginStart(System.AsyncCallback callback, object asyncState)
        {
            object[] _args = new object[0];
            System.IAsyncResult _result = base.BeginInvoke("Start", _args, callback, asyncState);
            return _result;
        }
        
        public void EndStart(System.IAsyncResult result)
        {
            object[] _args = new object[0];
            base.EndInvoke("Start", _args, result);
        }
        
        public System.IAsyncResult BeginStop(System.AsyncCallback callback, object asyncState)
        {
            object[] _args = new object[0];
            System.IAsyncResult _result = base.BeginInvoke("Stop", _args, callback, asyncState);
            return _result;
        }
        
        public void EndStop(System.IAsyncResult result)
        {
            object[] _args = new object[0];
            base.EndInvoke("Stop", _args, result);
        }
        
        public System.IAsyncResult BeginGetPersonLocation(System.AsyncCallback callback, object asyncState)
        {
            object[] _args = new object[0];
            System.IAsyncResult _result = base.BeginInvoke("GetPersonLocation", _args, callback, asyncState);
            return _result;
        }
        
        public FollowMe.Enums.TargetLocation EndGetPersonLocation(System.IAsyncResult result)
        {
            object[] _args = new object[0];
            FollowMe.Enums.TargetLocation _result = ((FollowMe.Enums.TargetLocation)(base.EndInvoke("GetPersonLocation", _args, result)));
            return _result;
        }
        
        public System.IAsyncResult BeginGetDangerLocation(System.AsyncCallback callback, object asyncState)
        {
            object[] _args = new object[0];
            System.IAsyncResult _result = base.BeginInvoke("GetDangerLocation", _args, callback, asyncState);
            return _result;
        }
        
        public FollowMe.Enums.TargetLocation EndGetDangerLocation(System.IAsyncResult result)
        {
            object[] _args = new object[0];
            FollowMe.Enums.TargetLocation _result = ((FollowMe.Enums.TargetLocation)(base.EndInvoke("GetDangerLocation", _args, result)));
            return _result;
        }
        
        public System.IAsyncResult BeginGetPersonAndDangerLocation(System.AsyncCallback callback, object asyncState)
        {
            object[] _args = new object[0];
            System.IAsyncResult _result = base.BeginInvoke("GetPersonAndDangerLocation", _args, callback, asyncState);
            return _result;
        }
        
        public FollowMe.WebService.PersonAndDangerLocation EndGetPersonAndDangerLocation(System.IAsyncResult result)
        {
            object[] _args = new object[0];
            FollowMe.WebService.PersonAndDangerLocation _result = ((FollowMe.WebService.PersonAndDangerLocation)(base.EndInvoke("GetPersonAndDangerLocation", _args, result)));
            return _result;
        }
    }
}
