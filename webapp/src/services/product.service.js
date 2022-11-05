import http from "../endpoints/api";
class ProductService {
    getAll() {
        return http.get("/Product");
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

export default new ProductService();