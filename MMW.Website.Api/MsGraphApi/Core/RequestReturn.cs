using System;

namespace MMW.Website.Api.MsGraphApi.Core
{
    internal class RequestReturn<T>
    {
        #region Static

        /// <summary>
        ///     Create a new <see cref="RequestReturn{T}" /> with Status <see cref="RequestStatus.Error" /> and an error message in
        ///     <see cref="RequestReturn{T}.Message" />
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        internal static RequestReturn<T> Error(string message)
        {
            return new RequestReturn<T>(message);
        }

        #endregion

        #region Public Deklaration

        /// <summary>
        ///     Response Content of type <typeparamref name="T" />
        /// </summary>
        public T Content { get; }

        /// <summary>
        ///     Duration of request
        /// </summary>
        private TimeSpan Duration { get; }

        /// <summary>
        ///     Status of the request
        /// </summary>
        private RequestStatus Status { get; }

        /// <summary>
        ///     Additional Message of the response. On error you will get here detail information.
        /// </summary>
        private string Message { get; }

        public bool IsSuccess => Status == RequestStatus.Success;
        public bool IsError => Status == RequestStatus.Error;

        #endregion

        #region Constructor

        /// <summary>
        ///     Default Constructor
        /// </summary>
        /// <param name="content"></param>
        /// <param name="duration"></param>
        /// <param name="status"></param>
        /// <param name="message"></param>
        internal RequestReturn(T content, TimeSpan duration, RequestStatus status, string message)
        {
            Content = content;
            Duration = duration;
            Status = status;
            Message = message;
        }

        /// <summary>
        ///     Private Constructor for <see cref="RequestReturn{T}.Error(string)" />
        /// </summary>
        /// <param name="message"></param>
        private RequestReturn(string message)
        {
            Status = RequestStatus.Error;
            Message = message;
        }

        #endregion
    }
}