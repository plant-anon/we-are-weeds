using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace FirstApp.Models.Abstract {
    public class JsonPayload<TType> {
        public const int MAX_EXCEPTION_DEPTH = 10;

        public TType Payload { get; set; }
        public string Raw { get; set; }
        public bool Errored { get; set; }

        private string _Exception;
        public string Exception {
            get {
                return _Exception;
            }
            set {
                Errored = true;
                _Exception = value;
            }
        }

        public void SetException(System.Exception exception) {
            int exceptionDepth = 1;
            string exMsgs = exception.Message;

            System.Exception innerException = exception;
            while(innerException.InnerException != null && exceptionDepth < MAX_EXCEPTION_DEPTH) {
                innerException = innerException.InnerException;
                exMsgs += "\n\rInner Exception: " + innerException.Message;
                exceptionDepth++;
            }

            this.Exception = exMsgs;
        }

        public string Serialize() {
            if(Payload != null) {
                Raw = JsonConvert.SerializeObject(Payload);
            } else {
                Raw = null;
            }

            return Raw;
        }

        public R To<R>() {
            return JsonConvert.DeserializeObject<R>(Raw);
        }

        public dynamic ToDynamic() {
            return new {
                Payload = Payload,
                Raw = Raw,
                Errored = Errored,
                Exception = _Exception
            };
        }
    }

	public class JsonPayload : JsonPayload<object> { }
}