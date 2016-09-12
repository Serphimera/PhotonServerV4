using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using PhotonCommon.Database.Domains;

namespace PhotonCommon.Database.Mappings
{
    public class ParameterTMap : ClassMap<ParameterT>
    {

        public ParameterTMap()
        {
            Table("parameter_t");
            LazyLoad();
            CompositeId().KeyProperty(x => x.ParCompGroup, "ParCompGroup")
                         .KeyProperty(x => x.ParComp, "ParComp")
                         .KeyProperty(x => x.ParKey, "ParKey");
            Map(x => x.ParValue).Column("ParValue").Not.Nullable();
            Map(x => x.ParValueMin).Column("ParValueMin").Not.Nullable();
            Map(x => x.ParValueMax).Column("ParValueMax").Not.Nullable();
            Map(x => x.ParDescription).Column("ParDescription").Not.Nullable();
            Map(x => x.dzins).Column("dzins").Not.Nullable();
            Map(x => x.dzupd).Column("dzupd").Not.Nullable();
            Map(x => x.namupd).Column("namupd").Not.Nullable();
            Map(x => x.Tsn).Column("Tsn").Not.Nullable();
        }
    }
}
