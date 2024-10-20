using UnityEngine;
using System;
using System.Collections.Generic;

public partial class HttpGameServer : MonoBehaviour
{
    public class ResErrorMessage
    {
        public string code;
        public int action;
        public string message;
    }

    public class Client
    {
        public class Item
        {
            public class Equip
            {
                public class Req
                {
                    public int itemId;
                }

                public class Res : ResErrorMessage
                {
                    public int result;
                    public List<Data.Item> items;
                }

                public static void Request(int _itemId, System.Action<Res> _res, bool _bIndicator = false)
                {
                    Req req = new Req();
                    req.itemId = _itemId;

                    string requestJson = Newtonsoft.Json.JsonConvert.SerializeObject(req);
                    RequestBase(WebURL + "item/equip", requestJson, false, _res, _bIndicator);
                }
            }
        }
    }
}

