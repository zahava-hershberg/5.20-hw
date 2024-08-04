import React, { useState, useEffect } from 'react';
import axios from 'axios';
import 'bootstrap/dist/css/bootstrap.min.css';
import './Home.css';

const Home = () => {

    const [news, setNews] = useState([]);

    const getNews = async () => {
        const { data } = await axios.get('/api/lakewoodscoop/scrape');
        setNews(data);
    }
    useEffect(() => {
        getNews();
    }, []);

    return (
        <div className='container' style={{ marginTop: 80 }}>
            <div className='row'>
                {news.map(n => (
                    <div className='col-md-4 mb-4' key={n.title}>
                        <div className='card h-100'>
                            <img src={n.image} className='card-img-top' alt={n.title} />
                            <div className='card-body'>
                                <h2>{n.comments} Comment/s</h2>
                                <h5 className='card-title'>
                                    <a href={n.url} target='_blank'>
                                        {n.title}
                                    </a>
                                </h5>
                                <p className='card-text'>{n.text}</p>
                            </div>
                        </div>
                    </div>
                ))}
            </div>
        </div>
    );
};

export default Home;
