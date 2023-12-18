namespace PulseGym.Entities.Exceptions
{
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message) { }

        public NotFoundException(string entity, Guid id) : base($"{entity} with Id = {id} not found") { }

        public NotFoundException(string entity, string property, string value)
            : base($"{entity} with {property} = {value} not found") { }
    }
}
