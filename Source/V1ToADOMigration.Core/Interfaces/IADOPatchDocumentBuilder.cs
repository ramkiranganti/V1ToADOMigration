using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.Services.WebApi.Patch.Json;

namespace V1ToADOMigration.Core.Interfaces
{
    public interface IADOPatchDocumentBuilder
    {
        IList<JsonPatchDocument> BuildJsonPatchDocuments();
    }
}
