﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace myLimo.ServiceReference2 {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="", ConfigurationName="ServiceReference2.Service2")]
    public interface Service2 {
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/DoWork", ReplyAction="urn:Service2/DoWorkResponse")]
        string DoWork();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/DoWork", ReplyAction="urn:Service2/DoWorkResponse")]
        System.Threading.Tasks.Task<string> DoWorkAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/DoTest", ReplyAction="urn:Service2/DoTestResponse")]
        string DoTest(string s);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/DoTest", ReplyAction="urn:Service2/DoTestResponse")]
        System.Threading.Tasks.Task<string> DoTestAsync(string s);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/SettingGetByName", ReplyAction="urn:Service2/SettingGetByNameResponse")]
        string SettingGetByName(int bizId, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/SettingGetByName", ReplyAction="urn:Service2/SettingGetByNameResponse")]
        System.Threading.Tasks.Task<string> SettingGetByNameAsync(int bizId, string name);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/CheckoutInsert", ReplyAction="urn:Service2/CheckoutInsertResponse")]
        int CheckoutInsert(int bizId, string username, int eltId, string name, int quantity, decimal unitPrice, decimal totalPrice);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/CheckoutInsert", ReplyAction="urn:Service2/CheckoutInsertResponse")]
        System.Threading.Tasks.Task<int> CheckoutInsertAsync(int bizId, string username, int eltId, string name, int quantity, decimal unitPrice, decimal totalPrice);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/CheckoutDeleteByEltId", ReplyAction="urn:Service2/CheckoutDeleteByEltIdResponse")]
        void CheckoutDeleteByEltId(int eltId, int bizId);
        
        [System.ServiceModel.OperationContractAttribute(Action="urn:Service2/CheckoutDeleteByEltId", ReplyAction="urn:Service2/CheckoutDeleteByEltIdResponse")]
        System.Threading.Tasks.Task CheckoutDeleteByEltIdAsync(int eltId, int bizId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface Service2Channel : myLimo.ServiceReference2.Service2, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class Service2Client : System.ServiceModel.ClientBase<myLimo.ServiceReference2.Service2>, myLimo.ServiceReference2.Service2 {
        
        public Service2Client() {
        }
        
        public Service2Client(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public Service2Client(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service2Client(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public Service2Client(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string DoWork() {
            return base.Channel.DoWork();
        }
        
        public System.Threading.Tasks.Task<string> DoWorkAsync() {
            return base.Channel.DoWorkAsync();
        }
        
        public string DoTest(string s) {
            return base.Channel.DoTest(s);
        }
        
        public System.Threading.Tasks.Task<string> DoTestAsync(string s) {
            return base.Channel.DoTestAsync(s);
        }
        
        public string SettingGetByName(int bizId, string name) {
            return base.Channel.SettingGetByName(bizId, name);
        }
        
        public System.Threading.Tasks.Task<string> SettingGetByNameAsync(int bizId, string name) {
            return base.Channel.SettingGetByNameAsync(bizId, name);
        }
        
        public int CheckoutInsert(int bizId, string username, int eltId, string name, int quantity, decimal unitPrice, decimal totalPrice) {
            return base.Channel.CheckoutInsert(bizId, username, eltId, name, quantity, unitPrice, totalPrice);
        }
        
        public System.Threading.Tasks.Task<int> CheckoutInsertAsync(int bizId, string username, int eltId, string name, int quantity, decimal unitPrice, decimal totalPrice) {
            return base.Channel.CheckoutInsertAsync(bizId, username, eltId, name, quantity, unitPrice, totalPrice);
        }
        
        public void CheckoutDeleteByEltId(int eltId, int bizId) {
            base.Channel.CheckoutDeleteByEltId(eltId, bizId);
        }
        
        public System.Threading.Tasks.Task CheckoutDeleteByEltIdAsync(int eltId, int bizId) {
            return base.Channel.CheckoutDeleteByEltIdAsync(eltId, bizId);
        }
    }
}