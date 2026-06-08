using System;
using System.Collections.Generic;
using System.Text;

namespace TaskFlow.Application.DTOs.Task
{
    public class CreateTaskRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
