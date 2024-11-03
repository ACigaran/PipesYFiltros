using System;
using CompAndDel.Filters;

namespace CompAndDel.Pipes
{
    public class PipeConditionalBranching : IPipe
    {
        private IFilter filter;
        private IPipe trueBranch;
        private IPipe falseBranch;

        public PipeConditionalBranching(IFilter filter)
        {
            this.filter = filter;
        }

        public void SetTrueBranch(IPipe trueBranch)
        {
            this.trueBranch = trueBranch;
        }

        public void SetFalseBranch(IPipe falseBranch)
        {
            this.falseBranch = falseBranch;
        }

        public IPicture Send(IPicture picture)
        {
            IPicture result = this.filter.Filter(picture);

            if (result != null)
            {
                if (this.trueBranch != null)
                {
                    return this.trueBranch.Send(picture);
                }
                else
                {
                    return picture; 
                }
            }
            else
            {
                if (this.falseBranch != null)
                {
                    return this.falseBranch.Send(picture);
                }
                else
                {
                    return picture; 
                }
            }
        }
    }
}