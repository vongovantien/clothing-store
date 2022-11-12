import FormAddProduct from "@components/FormAddProduct";
import StickyHeadTable from "@components/StickyHeadTable";
import { getProductPaging } from "@features/products/productSlice";
import { Button, Container, Stack } from "@mui/material";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { ProductConstants } from "src/utils/constants/ProductContants";



const columns = [
    { id: 'action', label: 'Hành động', minWidth: 170 },
    { id: 'code', label: 'Mã sản phẩm', minWidth: 170 },
    { id: 'title', label: 'Tên sản phẩm', minWidth: 170 },
    { id: 'price', label: 'Giá sản phầm', minWidth: 100 },
    { id: 'categoryId', label: 'Loại sản phẩm', minWidth: 170 }
];


const ProductPage = () => {
    const [show, setShow] = useState(false)
    const [productList, setProductList] = useState([])
    const [pageSize, setPageSize] = useState(5)
    const [pageNumber, setPageNumber] = useState(1)
    const dispatch = useDispatch()
    useEffect(() => {
        getProductList(pageSize, pageNumber);
    }, [pageSize, pageNumber])

    const getProductList = async (pageSize, pageNumber) => {

        dispatch(getProductPaging({ pageSize, pageNumber }))
            .unwrap()
            .then((res) => {
                setProductList(res.data)
            })
            .catch(res => console.error(res))
    }

    const handleOpenForm = () => {
        setShow(!show);
    }

    const handlePaging = (value, type) => {
        switch (type) {
            case ProductConstants.PAGE_SIZE:
                setPageSize(value)
                setPageNumber(1);
                break;
            case ProductConstants.PAGE_NUMBER:
                setPageNumber(value)
                break;
            default:
                break;
        }
    }

    return (
        <div>
            <FormAddProduct show={show} handleOpenForm={handleOpenForm} />
            <Stack direction="row" alignItems="center" justifyContent="flex-end" spacing={2} mr={2}>
                {/* <Button variant="contained" startIcon={<AddCircleOutlineIcon />} onClick={() => handleOpenForm()}>Thêm mới sản phẩm</Button>
                <Button variant="contained" startIcon={<ImportExportIcon />}>Import</Button> */}
                <Button variant="contained">Export</Button>
            </Stack>
            <h1 style={{ textAlign: "center" }}>Danh sách sản phẩm</h1>
            <Container>
                <StickyHeadTable columns={columns} rows={productList} pageSize={pageSize} pageNumber={pageNumber - 1} handlePaging={handlePaging} />
            </Container>
        </div >
    )
}

export default ProductPage
