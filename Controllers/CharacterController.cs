namespace dotnet_rpg.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _characterService;

        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAllCharacters")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>> Get()
        {
            // return the list of characters
            return await _characterService.GetAllCharacters();
        }

        [HttpGet("GetSingleCharacter/{id}")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>> GetSingle(int id)
        {
            return await _characterService.GetCharacterById(id);
        }

        [HttpPost("AddCharacter")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>>
                          AddCharacter(AddCharacterRequestDto newCharacter)
        {
            return Ok(await _characterService.AddCharacter(newCharacter));
        }

        [HttpPut("UpdateCharacter")]
        public async Task<ActionResult<ServiceResponse<GetCharacterResponseDto>>>
                          UpdateCharacter(UpdateCharacterRequestDto updateCharacter)
        {
            // call the service to update the Character
            ServiceResponse<GetCharacterResponseDto> sr = await _characterService.UpdateCharacter(updateCharacter);
            if (sr.Data is null)
            {
                return NotFound(sr);
            }

            return sr;
        }

        [HttpDelete("DeleteCharacter/{id}")]
        public async Task<ActionResult<ServiceResponse<List<GetCharacterResponseDto>>>>
                            DeleteCharacter(int id)
        {
            // call the Character Service to delete the character
            ServiceResponse<List<GetCharacterResponseDto>> sr = await _characterService.DeleteCharacter(id);
            if(sr.Data is null)
            {
                return NotFound(sr);
            }
            return sr;
        }
    }
}