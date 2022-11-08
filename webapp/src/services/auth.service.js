import http from "../endpoints/api";
class AuthService {
    getAll() {
        return http.get("/Product/GetAll");
    }
    getProductPaging(body) {
        return http.get(`/Product/GetProductPaging`, { body });
    }
    get(id) {
        return http.get(`/Product/${id}`);
    }

    create(data) {
        return http.post("/Product", data);
    }

    update(id, data) {
        return http.put(`/Product/${id}`, data);
    }

    delete(id) {
        return http.delete(`/Product/${id}`);
    }
}

export default new AuthService();