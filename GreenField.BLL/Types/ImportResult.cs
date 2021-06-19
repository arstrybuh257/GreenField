using System;
using System.Collections.Generic;

namespace GreenField.BLL.Dto
{
    public class ImportResult
    {
        public int SuccessfullyImported { get; set; }
        public int Failed { get; set; }
        public List<FailedImport> FailedImports { get; set; }

        public ImportResult()
        {
            SuccessfullyImported = 0;
            Failed = 0;
            FailedImports = new List<FailedImport>();
        }
    }

    public class FailedImport
    {
        public Guid Id { get; set; }
        public string Message { get; set; }

        public FailedImport(Guid id, string message)
        {
            Id = id;
            Message = message;
        }
    }
}