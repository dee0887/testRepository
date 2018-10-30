using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BootstrapSite4.Model
{
    /// <summary>
    /// Модель вложений пулл-реквеста
    /// </summary>
    public abstract class AttachmentAbstractModel
    {
        protected string _key; protected string _slug;
        public AttachmentAbstractModel(string key, string slug) { this._key = key; this._slug = slug; }
        public string Description { get; set; }
        public bool IsAttachment()
        {
            return !string.IsNullOrWhiteSpace(this.Description) && this.Description.Contains("attachment:");
        }
        protected string GetDescriptionFormatted()
        {
            if (!this.IsAttachment())
                return this.Description;
            return this.Description.Remove(this.Description.LastIndexOf(']') + 1);
        }
        public abstract HtmlString GetAttachment();
    }
}