﻿using EntityLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class FeatureRelation:IEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int FeatureId { get; set; }
    }
}
