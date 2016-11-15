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
    public static class EntityBaseExtensions
    {
        public static TTarget ToDto<TSource, TTarget>(this TSource source, Func<TSource, TTarget, TTarget> postMap = null)
            where TSource : EntityBase
            where TTarget : DtoBase
        {
            var target = Mapper.Map<TSource, TTarget>(source);
            var result = (postMap != null) ? postMap(source, target) : target;
            return result;
        }
    }
}
