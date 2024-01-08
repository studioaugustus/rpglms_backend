The AuthController receives a login request and passes the request data to a method in the AuthServices.
The AuthServices validates the request data and calls a method in the AuthRepository to fetch the user from the database.
The AuthRepository fetches the user from the database and returns the user to the AuthServices.
The AuthServices checks the password and, if it's correct, generates a token and returns it to the AuthController.
The AuthController sends the token back to the client in the HTTP response.