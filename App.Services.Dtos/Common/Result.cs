using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Dtos.Common
{
    public class Result
    {
        public virtual ResultStatusType Status { get; set; }
    }

    public abstract class BasicResult<TDto> : Result
    {
        protected internal TDto Data { get; set; }
        public IEnumerable<EntityAction> EntityActions { get; set; }
    }

    public abstract class ItemResult<TDto> : Result
        where TDto : DtoBase
    {
        protected internal TDto Data { get; set; }
        public IEnumerable<EntityAction> EntityActions { get; set; }
    }

    public abstract class ListResult<TDto, TItemResult> : Result
        where TDto : DtoBase
        where TItemResult : ItemResult<TDto>, new()
    {
        protected internal IEnumerable<TItemResult> Data { get; set; }

        public IEnumerable<EntityAction> EntityActions { get; set; }

        public Pagination Pagination { get; set; }
    }

    public sealed class SuccessResult : Result
    {
        public dynamic Data { get; set; }

        public SuccessResult()
        {
            Status = ResultStatusType.Success;
        }

        public SuccessResult(dynamic data)
        {
            Status = ResultStatusType.Success;
            Data = data;
        }
    }

    public sealed class FailResult : Result
    {
        public List<Error> Errors { get; private set; }

        public FailResult()
        {
            Status = ResultStatusType.Fail;
            Errors = new List<Error>();
        }
        public FailResult(Error error)
        {
            Status = ResultStatusType.Fail;
            Errors = new List<Error> { error };
        }

        public FailResult(List<Error> errors)
        {
            Status = ResultStatusType.Fail;
            Errors = errors;
        }
    }

    public sealed class IdentitySuccessResult<T> : BasicResult<T>
    {
        public IList<ResultStatus> ResultStatuses { get; private set; }

        public IdentitySuccessResult(T id)
        {
            var successStatus = new SuccessStatus();

            Status = successStatus.Code;

            ResultStatuses = new List<ResultStatus> { successStatus };

            Data = id;
        }

        public T Id { get { return Data; } }
    }
}
