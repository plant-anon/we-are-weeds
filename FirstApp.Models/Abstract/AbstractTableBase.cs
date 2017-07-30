using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace FirstApp.Models.Abstract {
    public abstract class AbstractTableBase {
        public bool IsActive { get; set; }
    }
}