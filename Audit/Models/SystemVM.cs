using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class OrgListRequest : DataTableAjaxPostModel
    {
        public int ORG_ID { get; set; }
        public string DEPARTMENT_NAME { get; set; }
        public string ORG_REGISTER_NO { get; set; }
        public string ORG_NAME { get; set; }
        public string ORG_CODE { get; set; }
        public string BUDGET_TYPE_NAME { get; set; }
        public string VIOLATION_DETAIL { get; set; }
        public string ORG_CONCENTRATOR_NAME { get; set; }
        public string STATUS_NAME { get; set; }
        public string INFORMATION_DETAIL { get; set; }
        public int[] status { get; set; }
        public List<string> violation { get; set; }
        public int? DeparmentID { get; set; }
    }
    public class OrgListResponse : DataTableAjaxResponModel
    {
        public List<OrgList> data { get; set; } = new List<OrgList>();
    }
    public class BM8ListRequest : DataTableAjaxPostModel
    {
        public string ChipNo { get; set; }
        public string FatherNo { get; set; }
        public string MotherNo { get; set; }
        public string HorseName { get; set; }
        public int BreedID { get; set; }
        public string Origin { get; set; }
        public char Gender { get; set; }
        public int BirthYear { get; set; }
        public int BirthMonth { get; set; }
        public string HorseColor { get; set; }
    }
    public class BM8ListResponse : DataTableAjaxResponModel
    {
        public List<BM8> data { get; set; } = new List<BM8>();
    }
    public class DataTableAjaxResponModel
    {
        public int draw { get; set; }
        public int recordsTotal { get; set; }
        public int recordsFiltered { get; set; }
        public bool Status { get; set; }
        public string error { get; set; }
        public string Message { get; set; }
    }

    public class DataTableAjaxPostModel
    {
        // properties are not capital due to json mapping
        public int draw { get; set; }
        public int start { get; set; }
        public int length { get; set; }
        public List<Column> columns { get; set; }
        public Search search { get; set; }
        public List<Order> order { get; set; }
    }

    public class Column
    {
        public string data { get; set; }
        public string name { get; set; }
        public bool searchable { get; set; }
        public bool orderable { get; set; }
        public Search search { get; set; }
    }

    public class Search
    {
        public string value { get; set; }
        public string regex { get; set; }
    }

    public class Order
    {
        public int column { get; set; }
        public string dir { get; set; }
    }
}