import AddCircleOutlineIcon from '@mui/icons-material/AddCircleOutline';
import DeleteIcon from '@mui/icons-material/Delete';
import ImportExportIcon from '@mui/icons-material/ImportExport';
import { Button, Container, Stack } from "@mui/material";
import { confirm } from 'components/ConfirmAlert';
import FormAddProduct from "components/FormAddProduct";
import StickyHeadTable from "components/StickyHeadTable";
import { ProductConstants } from "constants/ProductConstants";
import { getProductPaging } from "features/productSlice";
import { useEffect, useState } from "react";
import { useDispatch } from "react-redux";
import { toast } from 'react-toastify';
import productService from 'services/product.service';

const columns = [
    { id: '', label: '', minWidth: 170, },
    { id: 'code', label: 'Mã sản phẩm', minWidth: 170, format: (value) => value.toFixed(2), },
    { id: 'title', label: 'Tên sản phẩm', minWidth: 170, format: (value) => value.toFixed(2), },
    { id: 'price', label: 'Giá sản phầm', minWidth: 100, format: (value) => value.toFixed(2), },
    { id: 'categoryId', label: 'Loại sản phẩm', minWidth: 170, format: (value) => value.toFixed(2), },
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

    const onHandleDelete = async (id) => {
        if (await confirm("Are your sure?")) {
            handleDelete(id)
        }
    }

    const handleDelete = async (id) => {
        let result = await productService.delete(id)
        if (result.status === 200) {
            toast.success("Delete Product Successfully !!", {
                position: "bottom-right",
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "light",
                autoClose: 5000,
            });
            await getProductList(pageSize, pageNumber);
        }
        else {
            toast.error("Delete Product failure !!", {
                position: "bottom-right",
                hideProgressBar: false,
                closeOnClick: true,
                pauseOnHover: true,
                draggable: true,
                progress: undefined,
                theme: "light",
                autoClose: 5000,
            });
        }
    }
    const onHandleDetail = (id) => {
        console.log(id)
    }

    return (
        <div>
            <FormAddProduct show={show} handleOpenForm={handleOpenForm} />
            <Stack direction="row" alignItems="center" justifyContent="flex-end" spacing={2} mr={2}>
                <Button variant="contained" startIcon={<AddCircleOutlineIcon />} onClick={() => handleOpenForm()}>Thêm mới sản phẩm</Button>
                <Button variant="contained" startIcon={<ImportExportIcon />}>Import</Button>
                <Button variant="contained" startIcon={<DeleteIcon />}>Export</Button>
            </Stack>
            <h1 style={{ textAlign: "center" }}>Danh sách sản phẩm</h1>
            <Container>
                <StickyHeadTable columns={columns} rows={productList} pageSize={pageSize} pageNumber={pageNumber - 1} handlePaging={handlePaging} handleDelete={onHandleDelete} handleDetail={onHandleDetail} />
            </Container>
        </div >
    )
}

export default ProductPage
