﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;

internal static class Config
{
    readonly static string s_config = "configuration";
    internal static int GetNextProductId()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config),"NextProductId")?? throw new NullReferenceException();
    }
    public static int GetNextOrderId()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config),"NextOrderId") ?? throw new NullReferenceException();
    }
    public static int GetNextOrderItemId()
    {
        return XMLTools.ToIntNullable(XMLTools.LoadListFromXMLElement(s_config),"NextOrderItemId") ?? throw new NullReferenceException();
    }
    public static void SetNextProductId(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextProductId")?.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root,s_config);
    }
    public static void SetNextOrderId(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderId")?.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }
    public static void SetNextOrderItemId(int id)
    {
        XElement root = XMLTools.LoadListFromXMLElement(s_config);
        root.Element("NextOrderItemId")?.SetValue(id.ToString());
        XMLTools.SaveListToXMLElement(root, s_config);
    }
}
