using System;
using System.ComponentModel.DataAnnotations;

namespace Modules.DatabaseModule.Tables;

public abstract class BaseEntity
{
    [Timestamp]
    public DateTime CreatedAt { get; set; }
}