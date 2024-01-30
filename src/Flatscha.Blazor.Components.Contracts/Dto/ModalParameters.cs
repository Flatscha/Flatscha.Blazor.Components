using System.Collections;
using System.Collections.ObjectModel;

namespace Flatscha.Blazor.Components.Contracts.Dto
{
    public class ModalParameters : IEnumerable<KeyValuePair<string, object?>>
    {
        private readonly Dictionary<string, object?> _parameters = [];

        public ReadOnlyDictionary<string, object?> Parameter => this._parameters.AsReadOnly();

        public ModalParameters()
        {

        }

        public ModalParameters(Dictionary<string, object?> parameters)
        {
            this._parameters = parameters;
        }

        public ModalParameters(params (string, object?)[] parameters)
        {
            this._parameters = parameters.ToDictionary();
        }

        public ModalParameters Add(string parameter, object? value)
        {
            this._parameters[parameter] = value;
            return this;
        }

        public T Get<T>(string parameter)
        {
            if (!this._parameters.TryGetValue(parameter, out var value)) { throw new ArgumentException($"{parameter} was not found in current parameters"); }

            if (value is not T typedValue) { throw new InvalidOperationException($"Parameter [{parameter}] is of type [{value?.GetType()}] but type [{typeof(T)}] was requested"); }

            return typedValue;
        }

        public T? TryGet<T>(string parameterName) => this._parameters.TryGetValue(parameterName, out var value) && value is T typedValue ? typedValue : default;

        public IEnumerator<KeyValuePair<string, object?>> GetEnumerator() => this._parameters.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this._parameters.GetEnumerator();
    }
}
