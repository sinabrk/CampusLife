using System.Collections.Generic;
using BG.CampusLife.Application.Interfaces;
using BG.CampusLife.Domain.Entities;
using BG.CampusLife.Domain.Enums;

namespace BG.CampusLife.Application.Common
{
    public class Result<T>
    {
        public bool Succeeded { get; set; }
        public ResultStatusCodes StatusCode { get; set; }
        public string Message { get; set; }
        public int Total { get; set; }
        public bool HasMore { get; set; }
        public T Entity { get; set; }
        public List<T> Entities { get; set; }
    }
}