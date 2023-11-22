namespace dotnet_rpg.Services.CharacterService
{
    // implementation of ICharacterService Interface
    public class CharacterService : ICharacterService
    {
        // create the list of characters
        private static List<Character> lstCharacters = new List<Character>(){
            new Character(),
            new Character { Name = "Sam", Id = 1} // no constructor to set the name, name is set this way
        };
        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
            _mapper = mapper;
        }

        // adds one character to the list
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> 
                                AddCharacter(AddCharacterRequestDto newCharacter)
        {
            // create a new characer
            Character character = _mapper.Map<Character>(newCharacter);
            character.Id = lstCharacters.Max(c => c.Id) + 1;

            // add the new character to the list of characters List
            lstCharacters.Add(character);

            // create a new Service Response
            ServiceResponse<List<GetCharacterResponseDto>> serviceResponse = new();
            // now return the service response
            serviceResponse.Data = lstCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return serviceResponse;
        }

        // returns the list of characters
        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> GetAllCharacters()
        {
            // create a new service resposne
            ServiceResponse<List<GetCharacterResponseDto>> serviceRes = new()
            {
                Data = lstCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList()
            };
            return serviceRes;
        }

        // returns one character from the list based on id
        public async Task<ServiceResponse<GetCharacterResponseDto>> GetCharacterById(int id)
        {
            // create a new Service Resposne
            ServiceResponse<GetCharacterResponseDto> servRes = new();

            // get a single character from the list
            Character? character = lstCharacters.FirstOrDefault(c => c.Id == id);
            
            // return the service res
            servRes.Data = _mapper.Map<GetCharacterResponseDto>(character);
            return servRes;
        }

        // update character
        public async Task<ServiceResponse<GetCharacterResponseDto>> UpdateCharacter(UpdateCharacterRequestDto updatedCharacter)
        {
            ServiceResponse<GetCharacterResponseDto> sr = new();
            try
            {
                // get the character to update from list of characters; based on id
                Character? character = lstCharacters.FirstOrDefault(c => c.Id == updatedCharacter.Id)!;

                if (character is null)
                {
                    throw new Exception($"Character with id {updatedCharacter.Id} could not be found");
                }

                // update character properties manually
                character.Name = updatedCharacter.Name;
                character.HitPoints = updatedCharacter.HitPoints;
                character.Strength = updatedCharacter.Strength;
                character.Defense = updatedCharacter.Defense;
                character.Intelligence = updatedCharacter.Intelligence;
                character.Class = updatedCharacter.Class;

                sr.Data = _mapper.Map<GetCharacterResponseDto>(character); 
            }
            catch (Exception ex)
            {
                sr.Message = ex.Message;
            }

            return sr;
        }

        public async Task<ServiceResponse<List<GetCharacterResponseDto>>> DeleteCharacter(int id)
        {
            // get the characters from the list of characters
            Character? character = lstCharacters.FirstOrDefault(c => c.Id == id);
            if (character is null)
            {
                throw new Exception($"Character with id {id} could not be found");
            }
            // remove the characters
            lstCharacters.Remove(character);

            // create a new Service Response and return it
            ServiceResponse<List<GetCharacterResponseDto>> sr = new();
            sr.Data = lstCharacters.Select(c => _mapper.Map<GetCharacterResponseDto>(c)).ToList();
            return sr;
        }

    }
}