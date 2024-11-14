namespace EZip.Model
{
    public enum RequestType
    {
        k_first,
        k_second 
    }

    public class AppRequest
    {
        /// <summary>
        /// The type of request
        /// </summary>
        public RequestType RequestType { get; set; }

        /// <summary>
        /// The time at which the request will timeout.if null, the request will not timeout.
        /// </summary>
        public DateTime ? Timeout { get; set; }

        /// <summary>
        /// Stores any additional data related to the request, similar to a void* in C.
        /// </summary>
        public object? RequestData { get; set; }
    }
}

