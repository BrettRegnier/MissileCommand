﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace missile_command
{
	class Entity
	{
		protected Body Body { get; private set; }
		protected Collider Collider { get; private set; }
	}
}
