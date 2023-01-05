import http from "../endpoints/api";
class AuthService {
    authentication(data) {
        return http.post("/Authenticate/signIn", data)
    }
}

export default new AuthService();