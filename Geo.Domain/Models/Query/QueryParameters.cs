using Geo.Rest.Domain.Constants;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Geo.Rest.Domain.Extensions;
using System.Collections.ObjectModel;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Threading;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Geo.Rest.Domain.Models.Query
{
    public abstract class QueryParameters
    {
        /// <summary>
        /// The two letter ISO code for the language, optional. Possible values: br, de, es, fa, fr, hr, it, ja, kr, nl, pt, zh. If an invalid value was given the default of "en" will be used.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string Language { get; set; }

        /// <summary>
        /// A pipe-seperated list of the fields that should be returned (e.g. name|id|createdAt).
        /// </summary>
        /// <value>
        /// The fields.
        /// </value>
        public string Fields { get; set; }

        [BindNever]
        public ICollection<string> FieldsList => this.Fields?.Split("|").ToCollection() ?? new Collection<string>();
    }
}
