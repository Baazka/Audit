﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;

namespace Audit.Models
{
    public class Bank
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Bank FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class Alba
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Alba FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class NDBaiguullaga
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public NDBaiguullaga FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }

    public class Sankhuujilt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Sankhuujilt FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class TusuwZakhiragch
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public TusuwZakhiragch FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class ZardlinAngilal
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public ZardlinAngilal FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class UilAjillagaa
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public UilAjillagaa FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class SankhuuTailan
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public SankhuuTailan FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class Tatwar
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Tatwar FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class Khelber
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Khelber FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class Khoroo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Khoroo FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
    public class Aimag
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Soum> Soums { get; set; }

        public Aimag FromXml(XElement elem)
        {
            this.ID = Convert.ToInt32(elem.Element("ID").Value);
            this.Name = elem.Element("Name").Value;
            this.Soums = (from item in elem.Elements("AimagSoum") select new Soum().FromXml(item)).ToList<Soum>();

            return this;
        }
    }
    public class Soum
    {
        public int ID { get; set; }
        public int AimagID { get; set; }
        public string Name { get; set; }

        public Soum FromXml(XElement elem)
        {
            this.ID = Convert.ToInt32(elem.Element("ID").Value);
            this.Name = elem.Element("Name").Value;
            this.AimagID = Convert.ToInt32(elem.Element("AimagID").Value);

            return this;
        }
    }

    public class Status
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Status FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }

    public class Error
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public Error FromXml(XElement elem)
        {
            if (elem.Element("ID") != null)
                ID = Convert.ToInt32(elem.Element("ID").Value);
            if (elem.Element("Name") != null)
                Name = elem.Element("Name").Value;
            return this;
        }
    }
}