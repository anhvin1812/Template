using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Entities;
using App.Services.Dtos.Common;
using AutoMapper;

namespace App.Services.Common
{
    internal static class DtoExtensions
    {
        public static TTarget ToEntity<TSource, TTarget>(this TSource source, TTarget target = null, Func<TSource, TTarget, TTarget> postMap = null)
            where TSource : DtoBase
            where TTarget : EntityBase
        {
            var mappedTarget = target == null ? Mapper.Map<TSource, TTarget>(source) : Mapper.Map<TSource, TTarget>(source, target);
            var result = (postMap != null) ? postMap(source, mappedTarget) : mappedTarget;
            return result;
        }
    }
}
