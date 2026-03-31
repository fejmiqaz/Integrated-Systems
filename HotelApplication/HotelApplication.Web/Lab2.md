### Additional elements have been added:

- PaginatedRequest
- PaginatedResult
- PaginatedRespond

An extension for mapping the result to the response in the PaginatedResultMappingExtensions class.

A function in the Repository GetAllPagedAsync that returns a PaginatedResult Interface IExamSlotService and an empty implementation of the same service.
- CurrentUserService
- AuditInterceptor
- AuthController

The goal of this lab is to implement the entire data flow:

#### Controller -> Mapper -> Service -> Repository -> DB and back

## Tasks:

Create a DTO RoomDto that is used in the InsertAsync and UpdateAsync methods in RoomService
Implement all methods in RoomService
- GetByIdNotNullAsync(Guid id): Task - returns a single entity, never null — appropriate exception handling is required
- GetAllAsync(int? status): Task> - returns a list of entities; if status is not null, apply filter
- InsertAsync(RoomDto dto): Task - receives DTO and returns saved entity
- UpdateAsync(Guid id, RoomDto dto): Task - receives DTO and returns updated entity
- DeleteAsync(Guid id): Task - deletes entity and returns the deleted one
- GetPagedAsync(int pageNumber, int pageSize): Task> - returns specific page from database
- This method should also return data for all Reservation objects in the collection
- Lazy Load should not be used

Create RoomMapper that intercepts requests from Web layer, calls service methods and maps data in both directions.
The mapper should contain methods for calling ALL service methods.
- Create a record RoomResponse which will have data on the room identifier, number, capacity, status, price per night, the identifier of the hotel where the room is located, as well as its name.
- Create a record ReservationResponse which will have data on the reservation identifier, start and end dates, date of the hotel service (if any), the user identifier, the user's full name, the hotel service identifier and its price.
- Create a record RoomWithReservationsResponse that will contain all the data as RoomResponse, but will also store a List.
Create an ReservationMappingExtensions extension that maps:
- Reservation -> ReservationResponse
- List -> List Create an RoomMappingExtenstions extension that maps:
- Room -> RoomResponse
- Room -> RoomWithReservationsResponse
- List -> List CreateOrUpdateRoomRequest -> RoomDto
- PaginatedResult -> PaginatedResponse 

Create an RoomController on the path /api/rooms with the following endpoints:
- GET /{id} - returns one Room by ID
- GET /?status- returns all Rooms; supports optional status query parameter for filtering
- GET /?pageNumber=&pageSize - returns paginated results; required parameters: pageNumber, pageSize (PaginatedRequest)
- POST / - creates a new Room from the request body
- Only logged in users can create rooms.
- PUT /{id} - updates an existing Room with the data from the request body
- Only logged in users can update rooms.
- DELETE /{id} - deletes an existing Room by ID