using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotonCommon.Database.Domains
{
    public class ParameterT
    {
        public virtual string ParCompGroup { get; }
        public virtual string ParComp { get; }
        public virtual string ParKey { get; }
        public virtual string ParValue { get; set; }
        public virtual string ParValueMin { get; set; }
        public virtual string ParValueMax { get; set; }
        public virtual string ParDescription { get; set; }
        public virtual DateTime dzins { get; set; }
        public virtual DateTime dzupd { get; set; }
        public virtual string namupd { get; set; }
        public virtual int Tsn { get; set; }
        
        #region NHibernate Composite Key Requirements
        public override bool Equals(object obj)
        {
            ParameterT t = obj as ParameterT;
            if (t == null) return false;
            return ParCompGroup == t.ParCompGroup
                   && ParComp == t.ParComp
                   && ParKey == t.ParKey;
        }
        public override int GetHashCode()
        {
            int hash = GetType().GetHashCode();
            hash = (hash * 397) ^ ParCompGroup.GetHashCode();
            hash = (hash * 397) ^ ParComp.GetHashCode();
            hash = (hash * 397) ^ ParKey.GetHashCode();

            return hash;
        }
        #endregion
    }
}
