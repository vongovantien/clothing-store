import React, { Component } from "react";
import Slider from "react-slick";

function SampleNextArrow(props) {
    const { className, style, onClick } = props;
    return (
        <div
            className={className}
            style={{ ...style, display: "block", background: "red" }}
            onClick={onClick}
        />
    );
}

function SamplePrevArrow(props) {
    const { className, style, onClick } = props;
    return (
        <div
            className={className}
            style={{ ...style, display: "block", background: "green" }}
            onClick={onClick}
        />
    );
}

export default class CustomArrows extends Component {
    render() {
        const settings = {
            dots: true,
            infinite: true,
            slidesToShow: 3,
            slidesToScroll: 1,
            nextArrow: <SampleNextArrow />,
            prevArrow: <SamplePrevArrow />
        };

        const images = [{
            id: 1,
            url: "https://picsum.photos/200/300"
        },
        {
            id: 2,
            url: "https://picsum.photos/200/300"
        },
        {
            id: 3,
            url: "https://picsum.photos/200/300"
        },
        {
            id: 4,
            url: "https://picsum.photos/200/300"
        },
        {
            id: 5,
            url: "https://picsum.photos/200/300"
        }
        ]
        return (
            <div>
                <Slider {...settings}>
                    {images && images.map(item =>
                        <div key={item.id}>
                            <img src={item.url} alt={item.id} />
                        </div>
                    )}
                </Slider>
            </div>
        );
    }
}