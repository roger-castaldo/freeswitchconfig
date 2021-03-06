﻿using System;
using System.Collections.Generic;
using System.Text;
using Org.Reddragonit.BackBoneDotNet;
using Org.Reddragonit.EmbeddedWebServer.Interfaces;
using Org.Reddragonit.EmbeddedWebServer.Components.Message;
using sSite = Org.Reddragonit.EmbeddedWebServer.Interfaces.Site;
using Utility = Org.Reddragonit.FreeSwitchConfig.DataCore.Utility;
using System.Configuration;

namespace Org.Reddragonit.FreeSwitchConfig.Site.Handlers
{
    public class ModelHandler : IRequestHandler
    {
        public const string BACKBONE_PATH = "/resources/scripts/base/backbone.js";
        public const string JQUERY_PATH = "/resources/scripts/base/jquery.js";
        public const string JSON_PATH = "/resources/scripts/base/json.js";

        internal static bool CompressJS
        {
            get
            {
                return bool.Parse(ConfigurationSettings.AppSettings["Org.Reddragonit.FreeSwitchConfig.Site.Handlers.ModelHandler.CompressJS"]);
            }
        }

        #region IRequestHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public bool CanProcessRequest(HttpRequest request, sSite site)
        {
            return RequestHandler.HandlesURL(request.URL, request.Method);
        }

        public void ProcessRequest(HttpRequest request, sSite site)
        {
            RequestHandler.HandleRequest(new MappedRequest(request));
        }

        public void Init()
        {
            RequestHandler.Start(RequestHandler.StartTypes.ThrowInvalidExceptions, JQUERY_PATH, JSON_PATH, BACKBONE_PATH,new BackBoneLogger());
        }

        public void DeInit()
        {
            RequestHandler.Stop();
        }

        public bool RequiresSessionForRequest(HttpRequest request, sSite site)
        {
            return true;
        }

        #endregion
    }
}
