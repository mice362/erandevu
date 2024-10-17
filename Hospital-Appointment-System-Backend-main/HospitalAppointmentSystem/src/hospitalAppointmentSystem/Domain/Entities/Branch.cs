﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Branch : Entity<int>
{
    public Branch()
    {
    }

    public Branch(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public string Name { get; set; }
    public virtual ICollection<Doctor> Doctors { get; set; }
}
