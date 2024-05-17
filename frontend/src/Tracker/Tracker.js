import {useEffect, useState } from 'react';
import toast from 'react-hot-toast';

export function Tracker(){

    const[trackers, setTrackers] = useState([]);
    const[selectedTracker, setSelectedTracker] = useState(0);

    useEffect(() =>{
        //Format: TrackedItemId, Description, DateCreated
        fetch('/api/assets/base/' + baseId, {
            method: 'GET'
        })
        .then((response) => {
            setTrackers(JSON.parse(response));
        })
        .catch((error) => {
            toast('Error - see console');
            console.log(error);
        });
    })

    function onTrackerChange(trackerId){
        //fetch data for tracker
    }

    return (
        <div>
            <div className='App-title'>
                <h2>PRICE TRACKER</h2>
                The secret to economic divination is a great deal of data.
            </div>
            <br/>
            <div className='App-body'>
                <label className="Layout-heading">Select a tracker to view</label>
                <select value={selectedTracker} onChange={(e)=>{onTrackerChange(e.target.value)}}>
                    {
                        trackers.map(tracker => (
                            <option key={tracker.TrackedItemId} value={tracker.TrackedItemId}>
                                {tracker.Description}
                            </option>
                        ))
                    }
                </select>
            </div>
        </div>
    );
}