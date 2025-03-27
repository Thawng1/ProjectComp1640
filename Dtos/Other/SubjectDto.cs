﻿using ProjectComp1640.Dtos.Class;

namespace ProjectComp1640.Dtos.Other
{
    public class SubjectDto
    {
        public string SubjectName { get; set; }
        public string Information { get; set; }
        public List<CreateClassDto> Classes { get; set; } = new List<CreateClassDto>();
    }
}
