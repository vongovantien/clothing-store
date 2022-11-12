
import http from "../endpoints/api";
class MenuService {
    getAll() {
        return http.get("/Menu");
    }

    getProductPaging(body) {
        return http.get("/Menu/GetProductPaging", body);
    }

    get(id) {
        return http.get(`/Menu/${id}`);
    }

    create(data) {
        return http.post("/Menu", data);
    }

    update(id, data) {
        return http.put(`/Menu/${id}`, data);
    }

    delete(id) {
        return http.delete(`/Menu/${id}`);
    }
}

export default new MenuService();