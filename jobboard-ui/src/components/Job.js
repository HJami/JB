import React, { Component } from 'react';
import './Job.css';

class Job extends Component {

  render() {
    console.log(this.props);
    return (
      <div className="Job-Box">
        <div id="job_name">
          <b>job name: </b>{this.props.job.name}
        </div>
        <div id="job_location">
          <b>Location: </b>{this.props.job.location}
        </div>
        <div id="job_description">
          <span >
            <b>Description: </b>{this.props.job.description}
          </span >
        </div>
      </div>
    );

  }
};

export default Job;
