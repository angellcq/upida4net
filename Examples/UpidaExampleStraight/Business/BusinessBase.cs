﻿using Upida;

namespace UpidaExampleStraight.Business
{
    public class BusinessBase
    {
        protected IMapper mapper;

        public BusinessBase(IMapper mapper)
        {
            this.mapper = mapper;
        }
    }
}