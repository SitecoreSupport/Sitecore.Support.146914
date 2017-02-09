using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.Pipelines.LoggingIn;
using Sitecore.Security.Accounts;
using Sitecore.SecurityModel;
using Sitecore.Sites;
using System;
using System.Linq;

namespace Sitecore.Support.Pipelines
{
    public class ResolveClientLanguage
    {

        public virtual void Process(Sitecore.Pipelines.LoggingIn.LoggingInArgs args)
        {
            Sitecore.Diagnostics.Assert.ArgumentNotNull(args, "args");
            if (!string.IsNullOrWhiteSpace(args.StartUrl))
            {
                return;
            }
            Sitecore.Security.Accounts.User user = Sitecore.Security.Accounts.User.FromName(args.Username, false);
            string startUrl = user.Profile.StartUrl;
            if (!string.IsNullOrWhiteSpace(startUrl))
            {
                args.StartUrl = startUrl;
                return;
            }
            string startUrl2;
            using (new Sitecore.SecurityModel.SecurityDisabler())
            {
                startUrl2 = Sitecore.Sites.SiteContextFactory.GetSiteInfo("shell").VirtualFolder;
            }
            args.StartUrl = startUrl2;
        }
    }
}
