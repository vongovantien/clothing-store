import http from "../endpoints/api";
class ProductService {
    getAll() {
        return http.get("/Product/GetAll");
    }

    getProductPaging(pageSize, pageNumber) {
        return http.get(`/Product/GetProductPaging?pageSize=${pageSize}&pageNumber=${pageNumber}`);
    }

    getHotProduct(pageSize, pageNumber) {
        return http.get(`/Product/GetHotProduct?pageSize=${pageSize}&pageNumber=${pageNumber}`);
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