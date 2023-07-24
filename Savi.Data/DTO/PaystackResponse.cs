namespace Savi.Data.DTO
{
    public class PaystackResponse
    {
        public bool status { get; set; }
        public string message { get; set; }
        public PaystackData data { get; set; }

    }
    //    ValueKind = Object : "{"status":true,"message":"Verification successful",
    //    "data":{"id":2965432538,"domain":"test","status":"success","reference":"T470565688084910",
    //    "receipt_number":null,"amount":500000,"message":null,"gateway_response":"Successful",
    //    "paid_at":"2023-07-20T21:09:09.000Z","created_at":"2023-07-20T21:08:41.000Z",
    //    "channel":"card","currency":"NGN","ip_address":"197.210.79.92","metadata":
    //    {"referrer":"https://paystack.com/pay/-xe2yil5lp"},"log":{"start_time":1689887322,
    //        "time_spent":28,"attempts":1,"errors":0,"success":true,"mobile":false,"input":[],
    //        "history":[{"type":"action","message":"Attempted to pay with card","time":27},{"type":"success",
    //            "message":"Successfully paid with card","time":28}]},"fees":17500,"fees_split":null,
    //    "authorization":{"authorization_code":"AUTH_43rnek3esj","bin":"408408","last4":"4081",
    //        "exp_month":"12","exp_year":"2030","channel":"card","card_type":"visa ","bank":"TEST BANK",
    //        "country_code":"NG","brand":"visa","reusable":true,"signature":"SIG_E9iCbFvukSxs3AYkSw0t",
    //        "account_name":null,"receiver_bank_account_number":null,"receiver_bank":null},
    //"customer":{"id":131400684,"first_name":"Augustine","last_name":"Augustine",
    //        "email":"augustinendubuisicharles@gmail.com","customer_code":"CUS_d8t4itbm5oz8kqu",
    //        "phone":"08136582045","metadata":{},"risk_action":"default","international_format_phone":null},
    //"plan":null,"split":{},"order_id":null,"paidAt":"2023-07-20T21:09:09.000Z","createdAt":"2023-07-20T21:08:41.000Z",
    //    "requested_amount":500000,"pos_transaction_data":null,"source":null,"fees_breakdown":null,
    //    "transaction_date":"2023-07-20T21:08:41.000Z","plan_object":{},"subaccount":{}}}"

    public class PaystackData
    {
        public long id { get; set; }
        public string domain { get; set; }
        public string status { get; set; }
        public string reference { get; set; }
        public object receiptNumber { get; set; }
        public decimal amount { get; set; }
        public object message { get; set; }
        public string gateway_Response { get; set; }
        public DateTime paid_at { get; set; }
        public DateTime created_at { get; set; }
        public string channel { get; set; }
        public string currency { get; set; }
        public string ip_address { get; set; }
        public Metadata metadata { get; set; }
        public Log log { get; set; }
        public int fees { get; set; }
        public object feess_plit { get; set; }
        public Authorization authorization { get; set; }
        public Customer customer { get; set; }
        public object plan { get; set; }
        public Split split { get; set; }
        public object order_id { get; set; }
        public int requested_amount { get; set; }
        public object pos_transaction_data { get; set; }
        public object source { get; set; }
        public object fees_breakdown { get; set; }
        public DateTime transaction_date { get; set; }
        public PlanObject plan_object { get; set; }
        public Subaccount subaccount { get; set; }
    }

    public class Metadata
    {
        public string Referrer { get; set; }
    }

    public class Log
    {
        public int start_time { get; set; }
        public int time_spent { get; set; }
        public int attempts { get; set; }
        public int errors { get; set; }
        public bool success { get; set; }
        public bool mobile { get; set; }
        public List<object> input { get; set; }
        public List<History> history { get; set; }
    }

    public class History
    {
        public string type { get; set; }
        public string message { get; set; }
        public int time { get; set; }
    }

    public class Authorization
    {
        public string authorization_code { get; set; }
        public string bin { get; set; }
        public string last4 { get; set; }
        public string exp_month { get; set; }
        public string exp_year { get; set; }
        public string channel { get; set; }
        public string cardtype { get; set; }
        public string bank { get; set; }
        public string countrycode { get; set; }
        public string brand { get; set; }
        public bool reusable { get; set; }
        public string signature { get; set; }
        public object account_name { get; set; }
        public object receiver_bank_account_number { get; set; }
        public object receiver_bank { get; set; }
    }

    public class Customer
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string customer_code { get; set; }
        public string phone { get; set; }
        public Metadata metadata { get; set; }
        public string risk_action { get; set; }
        public object international_format_phone { get; set; }
    }

    public class Split
    {
    }

    public class PlanObject
    {
    }

    public class Subaccount
    {
    }

}
