﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Todolist.Enums;

namespace Todolist.Classes
{
    internal class Tasks
    {
        public DateTime CreatedTime { get; set; }
        public DateTime DueTime { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public PrioritysOptions Priority { get; set; }
         public override  string ToString()=>
            $"CreatedTime: {CreatedTime}\nDueTime: {DueTime}\nDescription: {Description}\nTitle: {Title}\nPriority: {Priority}";
        

    }
}
