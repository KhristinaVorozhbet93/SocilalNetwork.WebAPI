#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
using System.ComponentModel.DataAnnotations;

namespace SocialNetwork.HttpModels.HttpModels.Requests
{
    public class DeleteAccountRequest
    {
        [Required]
        public Guid Id { get; set; }
    }
}
