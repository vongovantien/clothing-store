
import http from "../endpoints/api";
class CategoryService {
    getAll() {
        return http.get("/Category/GetAll");
    }

    getProductPaging(body) {
        return http.get("/Category/GetProductPaging", body);
    }

    get(id) {
        return http.get(`/Category/${id}`);
    }

    create(data) {
        return http.post("/Category", data);
    }

    update(id, data) {
        return http.put(`/Category/${id}`, data);
    }

    delete(id) {
        return http.delete(`/Category/${id}`);
    }
}

export default new CategoryService();