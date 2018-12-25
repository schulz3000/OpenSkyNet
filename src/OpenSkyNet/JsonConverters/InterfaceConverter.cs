using Newtonsoft.Json.Converters;
using System;

namespace OpenSkyNet
{
    class InterfaceConverter<TInterface, TClass> : CustomCreationConverter<TInterface>
        where TClass : TInterface, new()
    {
        public override TInterface Create(Type objectType) => new TClass();
    }
}
