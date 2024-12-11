namespace EZip.Model
{
    public class AppResponse
    {
        /// <summary>
        /// is the response successful,if false, the error message will be in the message field
        /// </summary>
        public bool IsSuccessful;

        /// <summary>
        /// the error message if the response is not successful
        /// </summary>
        public string ? ErrorMessage;

        /// <summary>
        /// Stores any additional data related to the response, similar to a void* in C.
        /// </summary>
        public object? ResponseData { get; set; }
    }
}
