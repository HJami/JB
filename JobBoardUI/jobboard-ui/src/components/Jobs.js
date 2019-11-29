import React, { Component } from 'react';
import Job from './Job.js'
import './Job.css';

class Jobs extends Component {
    state = {
        jobs: [
            {
                name: 'name1',
                description: 'desc1',
                location: 'Sydney'
            },
            {
                name: 'name2',
                description: 'desc2',
                location: 'Melbourne'
            },
            {
                name: 'name3',
                description: 'desc3',
                location: 'Brisbane'
            },
            {
                name: 'name4',
                description: 'desc4',
                location: 'Hobart'
            }

        ]
    }
    render() {
        return (
            <div>
                {this.state.jobs.map(job => <Job key={job.name} job={job} />)}
            </div>
        );
    }
}

export default Jobs;