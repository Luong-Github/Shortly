﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shortly.Persistence.Options
{
    public class SqlServerRetryOptions
    {
        [Required, Range(5, 20)]
        public int MaxRetryCount { get; set; }
        [Required, Timestamp]
        public TimeSpan MaxRetryDelay { get; set; }
        public int[]? ErrorNumbersToAdd {  get; set; }
    }
}
