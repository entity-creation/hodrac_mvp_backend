using Hodrac_MVP_Backend.DTOs.Tag;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Model = Hodrac_MVP_Backend.Models;

namespace Hodrac_MVP_Backend.Mappers.Tag
{
    public static class TagMapper
    {
        public static Model.Tag FromJsonDtoToTag(this TagJsonDto tagDto)
        {
            return new Model.Tag
            {
                Key = tagDto.Key,
                TagName = tagDto.Name,
                TagDescription = tagDto.Description,
            };
        }

        public static ClientTagDto FromTagToClientDto(this Model.Tag tag)
        {
            return new ClientTagDto { TagName = tag.TagName };
        }
    }
}
